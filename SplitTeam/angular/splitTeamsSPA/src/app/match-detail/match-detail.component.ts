import { Component, OnInit } from '@angular/core';
import { MatchService } from '../_services/match.service';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { TeamService } from '../_services/team.service';
import { Team } from '../_modules/team';
import { PlayerService } from '../_services/player.service';
import { Player } from '../_modules/player';

@Component({
  selector: 'app-match-detail',
  templateUrl: './match-detail.component.html',
  styleUrls: ['./match-detail.component.css']
})
export class MatchDetailComponent implements OnInit {
  teams: Team[];
  teamAName: 'Choose Team Α';
  teamBName: 'Choose Team B';
  players: Player[];
  contentEditable = false;
  playerListForDraw: number[] = [];
  isChecked: boolean;
  role: string;
  constructor(private matchService: MatchService, private router: Router, private modalService: NgbModal,
    private toastr: ToastrService, private teamService: TeamService, private playerService: PlayerService) { }

  ngOnInit() {
    this.role = localStorage.getItem('role').toString();
    this.getAllPayers();
    this.getAllTeamsWithoutTeamPlayers();
  }
  getAllTeamsWithoutTeamPlayers() {
    this.teamService.getAllTeamsWithoutTeamPlayers().subscribe((data: Team[]) => {
      this.teams = data;
    }, error => {
      console.log(error);
      this.toastr.error(error.console.error);
    });
  }
  createMatch() {
    const teamA = this.teams.find((item) => item.name === this.teamAName);
    const teamB = this.teams.find((item) => item.name === this.teamBName);
    this.teamService.splitTeams(teamA.id, teamB.id, this.playerListForDraw).subscribe(() => {
      this.router.navigate(['/matches']);
    }, error => {
      this.toastr.error(error.error);
    });
  }
  getAllPayers() {
    this.playerService.getAllPayers().subscribe((data: Player[]) => {
      this.players = data;
    }, error => {
    });
  }
  toggleEditable(event, Id: number) {
    if (event.target.checked) {
      this.playerListForDraw.push(Id);
    }
  }
}
