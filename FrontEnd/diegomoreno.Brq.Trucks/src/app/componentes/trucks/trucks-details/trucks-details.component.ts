import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TruckService } from 'src/app/services/truck.service';
import { TruckModel } from '../../../models/TruckModel';

@Component({
  selector: 'app-trucks-details',
  templateUrl: './trucks-details.component.html',
  styleUrls: ['./trucks-details.component.scss']
})
export class TrucksDetailsComponent implements OnInit {

  truckId: any;
  truck = {} as TruckModel;
  form: FormGroup = {} as FormGroup;
  stateSave = 'post';
  currentYear: number = new Date().getFullYear();

  get editMode(): boolean {
    return this.stateSave === 'put';
  }

  get f(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private router: Router,
    private truckService: TruckService) { }

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void{
    this.form = this.fb.group({
      serieYear: [
        '',
        [
          Validators.required,
          Validators.min(this.currentYear),
          Validators.max(this.currentYear+1)
        ],
      ],
      fabricationYear: [
        '',
        [
          Validators.required
        ]
      ],
      seriesEnum: ['', Validators.required]
    })
  }

  public resetForm(): void {
    this.loadTruck();
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public loadTruck(): void{
    this.truckId = this.activatedRouter.snapshot.paramMap.get('id');

    if(this.truckId !== null && this.truckId !== ''){
      this.stateSave = 'put';

        this.truckService
          .getById(this.truckId)
          .subscribe(
            (truck: TruckModel) =>{
              this.truck ={ ...truck};
              this.form.patchValue(this.truck);
            },
            (error: any) =>{
              console.error(error);
            }
          );
    }
  }

  public saveTruck(): void{
    if(this.form.valid){
      this.truck = { ...this.form.value };

      if(this.stateSave === 'post'){
        this.truckService.post(this.truck)
        .subscribe(
          (result: any) =>  {
          this.router.navigate(['truck']);
         });
      }

      if(this.stateSave === 'put'){
        this.truckService.put(this.truck)
          .subscribe(
            (result: any) =>  {
            this.router.navigate(['truck']);
           });
      }
    }
  }
}
