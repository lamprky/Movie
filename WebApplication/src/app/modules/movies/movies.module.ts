import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MoviesComponent } from './movies.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { MovieDataService } from '../services/movie-data.service';
import { MovieComponent } from './forms/movie/movie.component';
import { DetailsMapPipe } from '../pipes/details-map.pipe';
import { FormOptionsService } from '../services/form-options.service';

const routes: Routes = [
  { path: 'movies-list', component: MoviesComponent },
  { path: 'movie', component: MovieComponent }
];

@NgModule({
  declarations: [MoviesComponent, MovieComponent, DetailsMapPipe],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule,
  ],
  providers: [MovieDataService, FormOptionsService]

})
export class MoviesModule { }
