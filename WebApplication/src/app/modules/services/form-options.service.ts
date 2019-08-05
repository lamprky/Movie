import { Injectable } from '@angular/core';
import { SelectOption } from '../models/SelectOption';

@Injectable({
  providedIn: 'root'
})
export class FormOptionsService {

  private languageOptions: SelectOption[] = new Array<SelectOption>();

  constructor() {

    this.languageOptions.push({ value: 'English', key: "D4423635-10ED-416E-81EE-D5D7E6746090" });
    this.languageOptions.push({ value: 'Greek', key: "E621BA86-B5CF-4AAE-B867-56969E71851A" });
    this.languageOptions.push({ value: 'Italian', key: "3828E370-B755-47E8-8CCC-12EBCB1CEBA4" });
    this.languageOptions.push({ value: 'Spanish', key: "B6F6270D-F435-4BD4-8723-94B7659B0505" });
   }

   getLanguageOptions(): SelectOption[] {
    return this.languageOptions;
  }
}
