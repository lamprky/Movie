import { Pipe, PipeTransform } from '@angular/core';
import { SelectOption } from '../models/SelectOption';

@Pipe({
  name: 'keyValue'
})
export class KeyValuePipe implements PipeTransform {

  transform(key: any, options: SelectOption[]): any {
    const selectedOption = options.find(option => option.key.toUpperCase() === key.toUpperCase());
    if (selectedOption) {
      return selectedOption.value;
    } else {
      return key;
    }
  }

}
