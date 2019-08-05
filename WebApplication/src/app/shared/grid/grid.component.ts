import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { OrderBy } from 'src/app/modules/models/OrderBy';
import { FormOptionsService } from 'src/app/modules/services/form-options.service';
import { Detail } from 'src/app/modules/models/detail';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnInit, OnChanges {

  @Input() items: Detail[];
  orderBy: OrderBy = { isAsc: true, column: '' };
  public checkAll = false;

  constructor(public formOptionsService: FormOptionsService) {

  }

  ngOnInit() {
  }
  ngOnChanges() {
  }

  public checkAllItems(checked: boolean) {
    this.items.forEach(item => {
      item.checked = checked;
    });
  }

  getOrderClass(key: string): string {
    return this.orderBy.column === key
      ? this.orderBy.isAsc
        ? 'fa fa-angle-down'
        : 'fa fa-angle-up'
      : '';
  }
}
