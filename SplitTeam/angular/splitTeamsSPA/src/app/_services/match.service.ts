import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Match } from '../_modules/match';

@Injectable({
  providedIn: 'root'
})
export class MatchService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }
  createMatch(match: Match): Observable<Match> {
    return this.http.post<Match>(this.baseUrl + 'Match/Create', match);
  }
  getAllMatches(): Observable<Match[]> {
    return this.http.get<Match[]>(this.baseUrl + 'Match/GetAll');
  }
  updateMatch(match: Match): Observable<Match> {
    return this.http.put<Match>(this.baseUrl + 'Match/Update', match);
  }
  getMatchById(matchId: number): Observable<Match> {
    return this.http.get<Match>(this.baseUrl + 'Match/Get/' + matchId);
  }
}
