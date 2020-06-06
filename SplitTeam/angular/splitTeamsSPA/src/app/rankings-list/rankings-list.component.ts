import { Component, OnInit } from '@angular/core';
import { Player } from '../_modules/player';
import { PlayerService } from '../Services/player.service';
import { ToastrService } from 'ngx-toastr';
import { RankingService } from '../Services/ranking.service';
import { Ranking } from '../_modules/rankings';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { RankingForUpdateDto } from '../_modules/rankingForUpdateDto';
import { RankingsDTO } from '../_modules/rankingsDto';

@Component({
  selector: 'app-rankings-list',
  templateUrl: './rankings-list.component.html',
  styleUrls: ['./rankings-list.component.css']
})
export class RankingsListComponent implements OnInit {
  modalOptions: NgbModalOptions;
  avgRank: number;
  players: Player[];
  rankingForPlayer: Ranking;
  rankingDto: RankingsDTO = { playerId: 0, skillId: 0, rank: 0 };
  rankingsForPlayer: Ranking[];
  constructor(private playerService: PlayerService, private toastr: ToastrService, private playerRankService: RankingService) {
    this.modalOptions = {
      backdrop: 'static',
      backdropClass: 'customBackdrop'
    };
  }

  ngOnInit() {
    this.getAllPayers();
  }
  getAllPayers() {
    this.playerService.getAllPayers().subscribe((data: Player[]) => {
      this.players = data;
    }, error => {
    });
  }
  showRankings(playerId: number) {
    this.getAllRankingsForPlayer(playerId);
  }
  getavgRankForPlayer() {
    let sum = 0;
    this.rankingsForPlayer.forEach(list => {
      sum += list.rank;
    });
    this.avgRank = sum / this.rankingsForPlayer.length;
  }
  getAllRankingsForPlayer(playerId: number) {
    this.playerRankService.getAllRankingsForPlayer(playerId).subscribe((data: Ranking[]) => {
      this.rankingsForPlayer = data;
      this.getavgRankForPlayer();
    }, error => {
      this.toastr.error(error.message);
    });
  }

  updateRankForPlayer(rankId: number, rank: number, playerId: number) {
    this.rankingDto.rank = rank;
    this.playerRankService.updateRankingForPlayer(rankId, this.rankingDto).subscribe((data: Ranking) => {
      this.rankingForPlayer = data;
      this.getAllRankingsForPlayer(playerId);
    }, error => {
      this.toastr.error(error.message);
    });
  }

}
