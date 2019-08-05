import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { GridComponent } from './grid/grid.component';
import { GridActionsComponent } from './grid-actions/grid-actions.component';
import { TranslationsComponent } from './translations/translations.component';
import { KeyValuePipe } from '../modules/pipes/key-value.pipe';


@NgModule({
  declarations: [GridComponent, GridActionsComponent, TranslationsComponent, KeyValuePipe],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule
  ],
  exports: [
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    GridComponent,
    GridActionsComponent,
    TranslationsComponent
  ]
})
export class SharedModule { }
