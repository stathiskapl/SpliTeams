import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Player } from '../_modules/player';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(private http: HttpClient) { }
  baseUrl = environment.apiUrl;

  getAllPayers(): Observable<Player[]> {
    return this.http.get<Player[]>(this.baseUrl + 'Player/GetAll');
  }
  createPlayer(player: Player): Observable<Player> {
    return this.http.post<Player>(this.baseUrl + 'Player/Create', player);
  }
}
