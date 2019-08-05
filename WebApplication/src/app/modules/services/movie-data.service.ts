import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Movie } from '../models/Movie';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MovieDataService {
  constructor(private httpClient: HttpClient) { }

  private controller = "movies";
  private standarPath = environment.endpoint + '/' + this.controller;

  getMovies(): Observable<Array<Movie>> {
    //console.log(this.standarPath);
    return this.httpClient.get<Movie[]>(this.standarPath);
  }

  getMovie(id: string): Observable<Movie> {
    //console.log(this.standarPath + '/' + id);
    return this.httpClient.get<Movie>(this.standarPath + '/' + id);
  }

  postMovie(movie: Movie): Observable<Movie> {
    return this.httpClient.post<Movie>(this.standarPath, movie);
  }

  putMovie(movie: Movie): Observable<Movie> {
    return this.httpClient.put<Movie>(this.standarPath, movie);
  }

  deleteMovie(id: string): Observable<Movie> {
    return this.httpClient.delete<Movie>(this.standarPath + '/' + id);
  }
}
