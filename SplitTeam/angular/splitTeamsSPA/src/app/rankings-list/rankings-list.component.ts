import { Component, OnInit } from '@angular/core';
import { Player } from '../_modules/player';
import { PlayerService } from '../Services/player.service';
import { ToastrService } from 'ngx-toastr';
import { RankingService } from '../Services/ranking.service';
import { Ranking } from '../_modules/rankings';
import { RankingForUpdateDto } from '../_modules/rankingForUpdateDto';
import { RankingsDTO } from '../_modules/rankingsDto';

@Component({
  selector: 'app-rankings-list',
  templateUrl: './rankings-list.component.html',
  styleUrls: ['./rankings-list.component.css']
})
export class RankingsListComponent implements OnInit {
  players: Player[];
  rankingForPlayer: Ranking;
  rankingDto: RankingsDTO = { playerId: 0, skillId: 0, rank: 0 };
  rankingsForPlayer: Ranking[];
  constructor(private playerService: PlayerService, private toastr: ToastrService, private playerRankService: RankingService) { }

  ngOnInit() {
    this.getAllPayers();
    //this.getAllRankingsForPlayer(1053);
  }
  getAllPayers() {
    this.playerService.getAllPayers().subscribe((data: Player[]) => {
      this.players = data;
      console.log(this.players);
    }, error => {
      console.log(error.message);
    });
  }
  showRankings(playerId: number) {
    this.getAllRankingsForPlayer(playerId);
  }
  getAllRankingsForPlayer(playerId: number) {
    this.playerRankService.getAllRankingsForPlayer(playerId).subscribe((data: Ranking[]) => {
      this.rankingsForPlayer = data;
    }, error => {
      this.toastr.error(error.message);
    });
  }

  updateRankForPlayer(rankId: number, rank: number, playerId: number) {
    console.log(rankId);
    console.log(rank);
    console.log(playerId);
    this.rankingDto.rank = rank;
    this.playerRankService.updateRankingForPlayer(rankId, this.rankingDto).subscribe((data: Ranking) => {
      this.rankingForPlayer = data;
      this.getAllRankingsForPlayer(playerId);
    }, error => {
      this.toastr.error(error.message);
    });
  }
  // updateRankForPlayer() {

  //   this.playerRankService.updateRankingForPlayer(playerRankForPlayer).subscribe((data: Ranking) => {
  //     this.rankingForPlayer = data;
  //   }, error => {
  //     this.toastr.error(error.message);
  //   });
  // }

}
