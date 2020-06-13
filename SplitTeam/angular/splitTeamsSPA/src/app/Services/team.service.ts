import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Team } from '../_modules/team';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  createTeam(team: Team): Observable<Team> {
    return this.http.post<Team>(this.baseUrl + 'Team/Create', team);
  }
  getAllTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(this.baseUrl + 'Team/GetAll');
  }
}
