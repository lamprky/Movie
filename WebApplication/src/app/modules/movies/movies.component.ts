import { Component, OnInit } from '@angular/core';
import { MovieDataService } from '../services/movie-data.service';
import { Movie } from '../models/Movie';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss']
})
export class MoviesComponent implements OnInit {

  public movies: Movie[] = [];
  constructor(private dataService: MovieDataService) { }

  ngOnInit() {
    this.dataService.getMovies().subscribe(
      movies => {
        this.refreshList(movies);
      },
      error => {
        alert('Cannot retrieve data');
      }
    );
  }

  private refreshList(movies: Movie[]) {
    this.movies = movies;
  }

  public onDelete() {
    for (var i = 0; i < this.movies.length; i++) {
      for (var j = 0; i < this.movies[i].details.length; j++) {
        if (this.movies[i].details[j].checked) {
          this.dataService.deleteMovie(this.movies[i].id).subscribe(
            movies => {
            },
            error => {
              alert('Error');
            }
          );
          this.movies.splice(i, 1);
        }
      }
    }
  }
}
