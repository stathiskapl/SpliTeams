import { Component, OnInit } from '@angular/core';
import { Player } from '../_modules/player';
import { PlayerService } from '../_services/player.service';
import { ToastrService } from 'ngx-toastr';
import { RankingService } from '../_services/ranking.service';
import { Ranking } from '../_modules/rankings';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { RankingForUpdateDto } from '../_modules/rankingForUpdateDto';
import { RankingsDTO } from '../_modules/rankingsDto';
import { SkillService } from '../_services/skill.service';
import { Skill } from '../_modules/skill';
import { RankingToSave } from '../_modules/rankingToSave';

@Component({
  selector: 'app-rankings-list',
  templateUrl: './rankings-list.component.html',
  styleUrls: ['./rankings-list.component.css']
})
export class RankingsListComponent implements OnInit {
  modalOptions: NgbModalOptions;
  avgRank: number;
  userId: number;
  players: Player[];
  skills: Skill[];
  contentEditable = false;
  rankingForPlayer: Ranking;
  rankingDto: RankingsDTO = { playerId: 0, skillId: 0, rank: 0, userId: 0 };
  rankingToSaveDto: RankingToSave = { id: 0, playerId: 0, skillId: 0, userId: 0, rank: 0 };
  rankingsToSave: RankingToSave[] = [{ id: 0, playerId: 0, skillId: 0, userId: 0, rank: 0 }];
  rankingsForPlayer: Ranking[];
  rankingsForUser: Ranking[];
  ranksForPlayer: Ranking[];
  isChecked: boolean;
  constructor(
    private playerService: PlayerService,
    private toastr: ToastrService,
    private playerRankService: RankingService,
    private skillService: SkillService) {
    this.modalOptions = {
      backdrop: 'static',
      backdropClass: 'customBackdrop'
    };
  }

  ngOnInit() {
    this.getAllPlayers();
    this.getAllSkills();
    this.userId = +localStorage.getItem('userId');
    this.getAllRankingsForUser(this.userId);
  }
  getAllPlayers() {
    this.playerService.getAllPayers().subscribe((data: Player[]) => {
      this.players = data;
    }, error => {
    });
  }
  showRankings(playerId: number) {
    this.getAllRankingsForPlayer(playerId, this.userId);
  }
  getavgRankForPlayer() {
    let sum = 0;
    if (this.ranksForPlayer) {
      this.ranksForPlayer.forEach(list => {
        sum += list.rank;
      });
      this.avgRank = sum / this.ranksForPlayer.length;
    }

  }
  getAllRankingsForPlayer(playersId: number, usersId: number) {

    this.ranksForPlayer = this.rankingsForUser.filter(rank => rank.player.id === +playersId);
    const playerItem = this.players.find(item => item.id === +playersId);
    if (this.ranksForPlayer.length === 0) {
      this.skills.forEach(skillItem => {
        const playerRank: Ranking = {
          player: playerItem,
          skill: skillItem,
          id: 0,
          userId: usersId,
          rank: 0
        };
        this.ranksForPlayer.push(playerRank);
      });
    }
    this.getavgRankForPlayer();
    // this.playerRankService.getAllRankingsForPlayer(playerId).subscribe((data: Ranking[]) => {
    //   this.rankingsForPlayer = data;
    //   this.getavgRankForPlayer();
    // }, error => {
    //   this.toastr.error(error.message);
    // });
  }
  getAllRankingsForUser(userId: number) {
    this.playerRankService.getAllRankingsForUser(userId).subscribe((data: Ranking[]) => {
      this.rankingsForUser = data;
      this.getavgRankForPlayer();
    }, error => {
      this.toastr.error(error.message);
    });
  }
  savePlayerRanks() {
    console.log(this.ranksForPlayer);
    this.rankingsToSave.length = 0;
    for (const rank of this.ranksForPlayer) {
      this.rankingsToSave.push({ id: rank.id, playerId: rank.player.id, rank: rank.rank, userId: rank.userId, skillId: rank.skill.id });
    }
    this.playerRankService.savePlayerRanks(this.rankingsToSave).subscribe((isSuccededSaving: boolean) => {
      if (isSuccededSaving === true) {
        this.toastr.success('Saved Successfully');
        this.ngOnInit();
        this.rankingsToSave.length = 0;
        this.isChecked = false;
        this.contentEditable = false;
      } else {
        this.toastr.error('error');
        this.isChecked = false;
        this.contentEditable = false;
      }
    }, error => {
      this.toastr.error(error.message);
      this.isChecked = false;
      this.contentEditable = false;
    });
  }

  getAllSkills() {
    this.skillService.getAllSkills().subscribe((data: Skill[]) => {
      this.skills = data;
    }, error => {
    });
  }


  // updateRankForPlayer(rankId: number, rank: number, playerId: number) {
  //   this.rankingDto.rank = rank;
  //   this.playerRankService.updateRankingForPlayer(rankId, this.rankingDto).subscribe((data: Ranking) => {
  //     this.rankingForPlayer = data;
  //     this.getAllRankingsForPlayer(playerId);
  //   }, error => {
  //     this.toastr.error(error.message);
  //   });
  // }

  // constructRankViewModel() {

  // }
  toggleEditable(event) {
    if (event.target.checked) {
      this.contentEditable = true;
    }
    if (!event.target.checked) {
      this.contentEditable = false;
    }
  }
  change(event, rank: Ranking) {
    this.ranksForPlayer.find(rankForPlayer => rankForPlayer.skill.name === rank.skill.name).rank = +event.target.value;
  }

}
