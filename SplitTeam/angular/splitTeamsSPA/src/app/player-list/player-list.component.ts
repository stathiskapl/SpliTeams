import { Component, OnInit } from '@angular/core';
import { PlayerService } from '../_services/player.service';
import { Player } from '../_modules/player';
import { ToastrService } from 'ngx-toastr';
import { NgbModalOptions, ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent implements OnInit {
  role: string;
  closeResult: string;
  modalOptions: NgbModalOptions;
  constructor(private playerService: PlayerService, private toastr: ToastrService, private modalService: NgbModal, private userService: UserService) {
    this.modalOptions = {
      backdrop: 'static',
      backdropClass: 'customBackdrop'
    };
    this.role = localStorage.getItem('role').toString();
  }
  playerName = '';

  players: Player[];
  ngOnInit() {
    this.role = localStorage.getItem('role').toString();
    this.getAllPayers();
  }

  open(content, playerId) {
    this.modalService.open(content, this.modalOptions).result.then((result) => {
      if (result === 'Delete') {
        this.deletePlayerWithRanks(playerId);
      } else if (result === 'Exit') {
      }
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
  deletePlayer(playerId: number) {

    this.playerService.deletePlayer(playerId).subscribe(() => {
      this.getAllPayers();
      this.toastr.success('Player Deleted');
    },
      error => {
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
      });
  }

  getAllPayers() {
    this.playerService.getAllPayers().subscribe((data: Player[]) => {
      this.players = data;
    }, error => {
    });
  }

  deletePlayerWithRanks(playerId: number) {

    this.playerService.deletePlayerWithRanks(playerId).subscribe(() => {
      this.getAllPayers();
      this.toastr.success('Player Deleted');
    },
      error => {
        this.toastr.error('Error!', error.message);
      });
  }
}
