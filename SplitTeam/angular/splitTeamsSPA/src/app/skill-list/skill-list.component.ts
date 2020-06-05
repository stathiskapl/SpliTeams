import { Component, OnInit } from '@angular/core';
import { SkillService } from '../Services/skill.service';
import { Skill } from '../_modules/skill';

@Component({
  selector: 'app-skill-list',
  templateUrl: './skill-list.component.html',
  styleUrls: ['./skill-list.component.css']
})
export class SkillListComponent implements OnInit {
  role: string;
  constructor(private skillService: SkillService) { }
  skillName = '';

  skills: Skill[];
  ngOnInit() {
    this.role = localStorage.getItem('role').toString();
    this.getAllSkills();
  }
  addSkill() {
    const skillToadd: Skill = {};
    skillToadd.name = this.skillName;
    this.skillService.createSkill(skillToadd).subscribe((data: Skill) => {
      this.getAllSkills();
      this.skillName = '';
    },
      error => {
      });
  }

  getAllSkills() {
    this.skillService.getAllSkills().subscribe((data: Skill[]) => {
      this.skills = data;
    }, error => {
    });
  }
}
