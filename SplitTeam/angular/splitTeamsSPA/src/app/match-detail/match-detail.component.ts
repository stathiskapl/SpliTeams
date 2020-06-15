import { Component, OnInit } from '@angular/core';
import { MatchService } from '../_services/match.service';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { TeamService } from '../_services/team.service';
import { Team } from '../_modules/team';

@Component({
  selector: 'app-match-detail',
  templateUrl: './match-detail.component.html',
  styleUrls: ['./match-detail.component.css']
})
export class MatchDetailComponent implements OnInit {
  teams: Team[];
  teamAName: 'Choose Team Α';
  teamBName: 'Choose Team B';
  role: string;
  constructor(private matchService: MatchService, private router: Router, private modalService: NgbModal,
    private toastr: ToastrService, private teamService: TeamService) { }

  ngOnInit() {
    this.role = localStorage.getItem('role').toString();
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
  test() {
    const teamA = this.teams.find((item) => item.name === this.teamAName);
    const teamB = this.teams.find((item) => item.name === this.teamBName);
    console.log(teamA.id);
    console.log(teamB.id);
  }
}
