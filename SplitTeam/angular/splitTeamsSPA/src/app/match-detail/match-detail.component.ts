import { Component, OnInit } from '@angular/core';
import { MatchService } from '../_services/match.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { TeamService } from '../_services/team.service';
import { Team } from '../_modules/team';
import { PlayerService } from '../_services/player.service';
import { Player } from '../_modules/player';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Match } from '../_modules/match';

@Component({
  selector: 'app-match-detail',
  templateUrl: './match-detail.component.html',
  styleUrls: ['./match-detail.component.css']
})
export class MatchDetailComponent implements OnInit {
  matchForm: FormGroup;
  teams: Team[];
  teamAName: 'Choose Team Α';
  teamBName: 'Choose Team B';
  players: Player[];
  contentEditable = false;
  playerListForDraw: number[] = [];
  isChecked: boolean;
  matchId: number;
  match: Match;
  role: string;
  constructor(private fb: FormBuilder, private matchService: MatchService, private router: Router,
    private modalService: NgbModal, private route: ActivatedRoute,
    private toastr: ToastrService, private teamService: TeamService, private playerService: PlayerService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.matchId = +params.get('matchId');
    });


    this.role = localStorage.getItem('role').toString();
    this.getAllPayers();
    this.getAllTeamsWithoutTeamPlayers();
    if (this.matchId !== 0) {
      this.getMatchById(this.matchId);
    }
  }
  createMatchForm() {
    this.matchForm = this.fb.group({
      teamA: [this.match?.teamA?.name, Validators.required],
      teamB: [this.match?.teamB?.name, Validators.required],
      scoreTeamA: [this.match?.scoreTeamA, [Validators.required, Validators.minLength(0), Validators.maxLength(100)]],
      scoreTeamB: [this.match?.scoreTeamB, [Validators.required, Validators.minLength(0), Validators.maxLength(100)]],
      description: [this.match?.description, Validators.required],
    });
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
  updateMatch() {
    this.match = Object.assign({}, this.matchForm.value);
    const matchToUpdate: Match = {
      id: this.matchId,
      scoreTeamA: this.match.scoreTeamA,
      scoreTeamB: this.match.scoreTeamB,
      description: this.match.description
    };

    this.matchService.updateMatch(matchToUpdate).subscribe(() => {
      this.toastr.success('Match Updated!');
      this.router.navigate(['/matches']);
    },
      error => {
        this.toastr.error(error.error);
      });
    console.log(matchToUpdate);
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
  getMatchById(matchId: number) {
    this.matchService.getMatchById(matchId).subscribe((data: Match) => {
      this.match = data;
      this.createMatchForm();
    }, error => {
    });
  }
}

