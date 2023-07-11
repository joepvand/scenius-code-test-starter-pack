import { Component } from '@angular/core';
import { CalculationsService } from './calculations.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'scenius-codetest';
  public dataList: any[] = [];
  page = 1;
	pageSize = 10;
	collectionSize = () => this.dataList.length;

  constructor(
    private calculationsService: CalculationsService
  ) { 
    calculationsService.backingList$ = this.dataList;
  }

  ngOnInit() {
    this.calculationsService.addDataListener();
    this.calculationsService.getAllData();      
  }

  submitData(formula: any) {
    let body = {
      calculation: formula.calculation,
    }

    this.calculationsService.postData(body)
    .subscribe(response => {
        console.log(response)
  })
  }


}
