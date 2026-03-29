import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { Trainer } from '../models';

@Component({
  selector: 'app-trainers',
  templateUrl: './trainers.component.html',
  styleUrls: ['./trainers.component.css']
})
export class TrainersComponent implements OnInit {
  trainers: Trainer[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.apiService.getTrainers().subscribe(data => this.trainers = data);
  }
}
