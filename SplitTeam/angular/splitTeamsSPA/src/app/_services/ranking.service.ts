import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Ranking } from '../_modules/rankings';
import { RankingForUpdateDto } from '../_modules/rankingForUpdateDto';
import { RankingsDTO } from '../_modules/rankingsDto';
import { RankingToSave } from '../_modules/rankingToSave';

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
  getAllRankingsForPlayer(playerId: number): Observable<Ranking[]> {
    return this.http.get<Ranking[]>(this.baseUrl + 'PlayerRank/GetAllForPlayer/' + playerId);
  }
  updateRankingForPlayer(rankId: number, playerRankToUpdate: RankingsDTO): Observable<Ranking> {
    return this.http.put<Ranking>(this.baseUrl + 'PlayerRank/Update/' + rankId, playerRankToUpdate);
  }
  getAllRankingsForUser(userId: number): Observable<Ranking[]> {
    return this.http.get<Ranking[]>(this.baseUrl + 'PlayerRank/GetAllForUser/' + userId);
  }
  savePlayerRanks(rankings: RankingToSave[]): Observable<boolean> {
    return this.http.put<boolean>(this.baseUrl + 'PlayerRank/SavePlayerRanks', rankings);
  }
}
