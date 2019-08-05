import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { MoviesModule } from './modules/movies/movies.module';
import { GenreMapPipe } from './modules/pipes/genre-map.pipe';

@NgModule({
  declarations: [
    AppComponent,
    GenreMapPipe,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MoviesModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
