import { Component, OnInit } from '@angular/core';
import { TruckModel } from 'src/app/core/models/Truck';
import { TruckService } from 'src/app/core/services/truck.service';

@Component({
  selector: 'app-trucks',
  templateUrl: './trucks.component.html',
  styleUrls: ['./trucks.component.scss']
})
export class TrucksComponent implements OnInit {

  truckList: TruckModel[] = [];
  truckListFilter: TruckModel[] = [];
  private _filterList = '';

  public get filterList(): string{
    return this._filterList;
  }

  constructor(private truckService: TruckService) { }

  ngOnInit(): void {
    this.getAllTruck();
  }

  getAllTruck(): void{
    const observer ={
      next: (_trucks: TruckModel[]) =>{
        this.truckList = _trucks;
        this.truckListFilter = _trucks;
        },
        error: (error: any) => console.log(error)
    }
    this.truckService.getAll().subscribe(observer);
  }

  deleteTruck(idTruck: any){
    this.truckService.delete(idTruck);
  }
}
