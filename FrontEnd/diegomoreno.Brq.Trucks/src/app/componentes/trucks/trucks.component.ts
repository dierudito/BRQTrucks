import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TruckModel } from 'src/app/models/TruckModel';
import { TruckService } from 'src/app/services/truck.service';

@Component({
  selector: 'app-trucks',
  templateUrl: './trucks.component.html',
  styleUrls: ['./trucks.component.scss']
})
export class TrucksComponent implements OnInit {

  truckList: TruckModel[] = [];

  constructor(
    private truckService: TruckService,
    private router: Router) { }

  ngOnInit(): void {
    this.getAllTruck();
  }

  getAllTruck(): void{
    const observer ={
      next: (_trucks: TruckModel[]) =>{
        this.truckList = _trucks;
        },
        error: (error: any) => console.log(error)
    }
    this.truckService.getAll().subscribe(observer);
  }

  truckDetails(id: string): void{
    this.router.navigate([`truck/details/${id}`]);
  }

  deleteTruck(id: string): void{
    this.truckService
      .delete(id).subscribe(
        (result: any) => {this.router.navigate(['truck'])}
      );
  }
}
