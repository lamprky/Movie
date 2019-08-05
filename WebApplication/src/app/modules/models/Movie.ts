import { Detail } from './detail';

export interface Movie {
  id: string;
  details: Detail[];
  contributors: string[];
  genres: string[];
}
