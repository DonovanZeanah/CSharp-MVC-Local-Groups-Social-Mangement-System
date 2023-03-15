import { Injectable } from '@angular/core';
import { Skill } from '../models/skill';

@Injectable({
  providedIn: 'root'
})
export class SkillService {

  constructor() { }

  public getSkills(): Skill[] {
    let skill = new Skill();
    skill.id = 1;
    skill.name = "C#";
    skill.level = 5;
    //skill.tools = {}]
    skill.description = "C# is a general-purpose, multi-paradigm programming language encompassing strong typing, imperative, declarative, functional, generic, object-oriented (class-based), and component-oriented programming disciplines.";
    skill.function = "C# is used to create web apps, desktop apps, mobile apps, games, and much more.";
    skill.subjectField = "C# is used in many fields, including science, engineering, medicine, finance, and education.";
    return [skill];
  }
}
