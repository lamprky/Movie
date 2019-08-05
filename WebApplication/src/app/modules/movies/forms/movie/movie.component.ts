import { Component, OnInit, Input } from '@angular/core';
import { Detail } from 'src/app/modules/models/detail';
import { MovieDataService } from 'src/app/modules/services/movie-data.service';
import { Movie } from 'src/app/modules/models/Movie';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss']
})
export class MovieComponent implements OnInit {

  constructor(private movieDataService: MovieDataService) { }
  @Input() id: string;
  form: FormGroup;

  public movie: Movie = {
    id: "",
    details: [],
    contributors: [],
    genres: [],
  };

  public detailsAreVisible:boolean = true;
  public contributorsAreVisible:boolean = false;
  public genresAreVisible:boolean = false;

  ngOnInit() {
    if (this.id) {
      this.movieDataService.getMovie(this.id).subscribe(
        movie => {
          this.movie = movie;
        },
        error => {
          alert('Cannot retrieve data');
        }
      );
    }
  }


  detailsNext() {
    this.genresAreVisible = true;
    this.detailsAreVisible = false;
    this.contributorsAreVisible = false;
  }

  genresNext() {
    this.genresAreVisible = false;
    this.detailsAreVisible = false;
    this.contributorsAreVisible = true;
  }

  genresPrev() {
    this.genresAreVisible = false;
    this.detailsAreVisible = true;
    this.contributorsAreVisible = false;
  }

  contributorsPrev() {
    this.genresAreVisible = true;
    this.detailsAreVisible = false;
    this.contributorsAreVisible = false;
  }
}
