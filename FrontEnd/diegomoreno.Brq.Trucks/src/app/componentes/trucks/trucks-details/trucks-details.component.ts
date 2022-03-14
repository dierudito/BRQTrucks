import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TruckService } from 'src/app/services/truck.service';
import { TruckModel } from '../../../models/TruckModel';
import { SeriesModel } from '../../../models/SeriesModel';
import { SeriesService } from '../../../services/series.service';

@Component({
  selector: 'app-trucks-details',
  templateUrl: './trucks-details.component.html',
  styleUrls: ['./trucks-details.component.scss']
})
export class TrucksDetailsComponent implements OnInit {

  truckId: any;
  truck = {} as TruckModel;
  series: any[] = [];

  form: FormGroup;
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
    private truckService: TruckService,
    private seriesService: SeriesService) {
      this.form = new FormGroup({});
      this.loadSeries();
    }

  ngOnInit(): void {
    this.validation();
    this.loadTruck();
  }

  public validation(): void{
    this.form = this.fb.group({
      idSeries: ['', Validators.required],
      fabricationYear: [this.currentYear],
      serieYear: ['',
        [Validators.required, Validators.min(this.currentYear), Validators.max(this.currentYear+1)]],
    });
  }

  public resetForm(): void {
    this.loadTruck();
    this.form.reset();
  }

  public cancelUpdate(): void{
    this.router.navigate(['']);
  }

  public loadSeries(): void{
    this.seriesService.getAll()
      .subscribe(
        (series: any[]) =>{
          this.series = series;
        }
      )
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
        this.truckService.put(this.truck, this.truckId)
          .subscribe(
            (result: any) =>  {
            this.router.navigate(['truck']);
           });
      }
    }
  }
}
