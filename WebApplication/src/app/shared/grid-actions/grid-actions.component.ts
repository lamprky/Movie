import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-grid-actions',
  templateUrl: './grid-actions.component.html',
  styleUrls: ['./grid-actions.component.scss']
})
export class GridActionsComponent implements OnInit {

  @Output() delete = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  public onDelete() {
    this.delete.emit();
  }

}
