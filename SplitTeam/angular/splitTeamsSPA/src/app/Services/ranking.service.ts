import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Ranking } from '../_modules/rankings';

@Injectable({
  providedIn: 'root'
})
export class RankingService {

  constructor(private http: HttpClient) { }
  baseUrl = environment.apiUrl;

  getAllRankings(): Observable<Ranking[]> {
    return this.http.get<Ranking[]>(this.baseUrl + 'PlayerRank/GetAll');
  }
  createPlayerRank(playerRank: Ranking): Observable<Ranking> {
    return this.http.post<Ranking>(this.baseUrl + 'Player/Create', playerRank);
  }
}
