import { Component, OnInit } from '@angular/core';
import { MatchService } from '../_services/match.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Match } from '../_modules/match';
import { TeamService } from '../_services/team.service';
import { TeamPlayer } from '../_modules/teamPlayer';
import { NgbModalOptions, NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-match-list',
  templateUrl: './match-list.component.html',
  styleUrls: ['./match-list.component.css']
})
export class MatchListComponent implements OnInit {
  role: string;
  matches: Match[];
  teamPlayers: TeamPlayer[];
  closeResult: string;
  modalOptions: NgbModalOptions;
  sumAVGRank: number;
  constructor(private matchService: MatchService, private router: Router, private modalService: NgbModal,
    private toastr: ToastrService, private teamService: TeamService) {
    this.modalOptions = {
      backdrop: 'static',
      backdropClass: 'customBackdrop'
    };
    this.role = localStorage.getItem('role');
  }
  ngOnInit() {
    this.role = localStorage.getItem('role').toString();
    this.getAllMatches();
  }
  getAllMatches() {
    this.matchService.getAllMatches().subscribe((data: Match[]) => {
      this.matches = data;
    }, error => {
      console.log(error);
      this.toastr.error(error.console.error);
    });
  }
  getAllTeamPlayersForTeamId(teamId: number) {
    this.teamService.getAllTeamPlayersForTeamId(teamId).subscribe((data: TeamPlayer[]) => {
      this.teamPlayers = data;
      this.getSumAvgForTeam(this.teamPlayers);
    }, error => {
      console.log(error);
      this.toastr.error(error.error);
    });
  }

  open(content, teamId) {
    this.getAllTeamPlayersForTeamId(teamId);
    this.modalService.open(content, this.modalOptions).result.then((result) => {


      // if (result === 'Delete') {
      //   this.getAllTeamPlayersForTeamId(teamId);
      // } else if (result === 'Exit') {
      // }
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else if (reason === 'Cross click') {
      return `with: ${reason}`;
    } else {
      return `with: ${reason}`;
    }
  }
  getSumAvgForTeam(teamPl: TeamPlayer[]) {
    this.sumAVGRank = 0;
    for (let teamP of teamPl) {
      this.sumAVGRank = this.sumAVGRank + teamP.player.averageRank;
    }
  }
}
