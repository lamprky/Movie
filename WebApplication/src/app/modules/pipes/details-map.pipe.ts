import { Pipe, PipeTransform } from '@angular/core';
import { Detail } from '../models/detail';
import { Movie } from '../models/Movie';

@Pipe({
  name: 'detailsMap'
})
export class DetailsMapPipe implements PipeTransform {
  transform(movies: Movie[]): Detail[] {
    var gridMovies: Detail[] = [];
    movies.forEach(x => {
      var firstDetail = x.details[0];
      var gridDetail: Detail = {
        id: firstDetail.id,
        name: firstDetail.name,
        title: firstDetail.title,
        description: firstDetail.description,
        languageId: firstDetail.languageId,
        checked: false
      };
      gridMovies.push(gridDetail);
    });
    return gridMovies;
  }
}
