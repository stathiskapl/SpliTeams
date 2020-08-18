import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PlayerStatsDto } from '../_modules/playerStatsDto';

@Injectable({
  providedIn: 'root'
})
export class StatsService {
  baseUrl = environment.apiUrl;
constructor(private http: HttpClient) { }
getMatchesWithTeamsAndPlayers(): Observable<PlayerStatsDto[]> {
  return this.http.get<PlayerStatsDto[]>(this.baseUrl + 'Stat/GetMatchesWithTeamsAndPlayers');
}
}
