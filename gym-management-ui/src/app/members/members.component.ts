import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { Member, CreateMember, Trainer, Plan } from '../models';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {
  members: Member[] = [];
  trainers: Trainer[] = [
    { trainerId: 101, name: 'Mike Ross', specialization: 'Strength Training' },
    { trainerId: 102, name: 'Sarah Connor', specialization: 'Fitness & Cardio' },
    { trainerId: 103, name: 'Harvey Specter', specialization: 'Bodybuilding' }
  ];
  plans: Plan[] = [
    { planId: 201, planName: 'Monthly Starter', durationMonths: 1, price: 49 },
    { planId: 202, planName: 'Six Months Pro', durationMonths: 6, price: 199 },
    { planId: 203, planName: 'Annual Elite', durationMonths: 12, price: 349 }
  ];

  newMember: CreateMember = {
    name: '',
    age: 0,
    phone: '',
    trainerId: undefined,
    planId: 0
  };

  constructor(private apiService: ApiService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadData();
    this.route.queryParams.subscribe(params => {
      if (params['planId']) {
        this.newMember.planId = Number(params['planId']);
      }
    });
  }

  loadData(): void {
    this.apiService.getMembers().subscribe({
      next: data => this.members = data,
      error: err => console.error('Error fetching members:', err)
    });
    
    this.apiService.getTrainers().subscribe({
      next: data => {
        if (data && data.length > 0) {
          this.trainers = data;
        }
      },
      error: err => console.error('Error fetching trainers:', err)
    });

    this.apiService.getPlans().subscribe({
      next: data => {
        if (data && data.length > 0) {
          this.plans = data;
        }
      },
      error: err => console.error('Error fetching plans:', err)
    });
  }

  addMember(): void {
    if (!this.newMember.name || !this.newMember.phone || this.newMember.planId === 0) {
      alert('Please fill in all required fields.');
      return;
    }

    // Ensure numbers are properly typed
    this.newMember.age = Number(this.newMember.age);
    this.newMember.planId = Number(this.newMember.planId);
    if(this.newMember.trainerId) {
       this.newMember.trainerId = Number(this.newMember.trainerId);
    }

    this.apiService.addMember(this.newMember).subscribe({
      next: () => {
        this.loadData(); // Refresh list
        this.newMember = { 
          name: '', 
          age: 0, 
          phone: '', 
          trainerId: undefined, 
          planId: (this.plans.length > 0 ? this.plans[0].planId : 0) 
        }; // Reset form
      },
      error: (err) => {
        console.error('Error adding member:', err);
        alert('Failed to add member. Please check the console for details.');
      }
    });
  }

  deleteMember(id: number): void {
    if(confirm('Are you sure you want to delete this member?')) {
      this.apiService.deleteMember(id).subscribe(() => this.loadData());
    }
  }
}
