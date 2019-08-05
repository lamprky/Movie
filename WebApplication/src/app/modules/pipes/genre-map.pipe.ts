import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'genreMap'
})
export class GenreMapPipe implements PipeTransform {

  transform(value: any, ...args: any[]): any {
    return null;
  }

}
