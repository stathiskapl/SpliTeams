import { Component, OnInit } from '@angular/core';
import { PlayerService } from '../Services/player.service';
import { Player } from '../_modules/player';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent implements OnInit {

  constructor(private playerService: PlayerService, private toastr: ToastrService) { }
  playerName = '';

  players: Player[];
  ngOnInit() {
    this.getAllPayers();
  }
  deletePlayer(playerId: number) {

    this.playerService.deletePlayer(playerId).subscribe(() => {
      this.getAllPayers();
      this.toastr.success('Player Deleted');
    },
      error => {
        console.log(error.message);
        this.toastr.error('Error!', error.message);
      });
  }
  addPlayer() {
    const playerToadd: Player = {};
    playerToadd.name = this.playerName;
    this.playerService.createPlayer(playerToadd).subscribe((data: Player) => {
      this.getAllPayers();
      this.playerName = '';
    },
      error => {
        console.log(error.message);
      });
  }

  getAllPayers() {
    this.playerService.getAllPayers().subscribe((data: Player[]) => {
      this.players = data;
      console.log(this.players);
    }, error => {
      console.log(error.message);
    });
  }
}
