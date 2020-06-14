import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Skill } from '../_modules/skill';

@Injectable({
  providedIn: 'root'
})
export class SkillService {

constructor(private http: HttpClient) { }
baseUrl = environment.apiUrl;

  getAllSkills(): Observable<Skill[]> {
    return this.http.get<Skill[]>(this.baseUrl + 'Skill/GetAll');
  }
  createSkill(skill: Skill): Observable<Skill> {
    return this.http.post<Skill>(this.baseUrl + 'Skill/Create', skill);
  }
}
