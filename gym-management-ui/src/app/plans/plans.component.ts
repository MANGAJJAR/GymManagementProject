import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { Plan } from '../models';
import { Router } from '@angular/router';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.css']
})
export class PlansComponent implements OnInit {
  plans: Plan[] = [
    { planId: 201, planName: 'Monthly Starter', durationMonths: 1, price: 49 },
    { planId: 202, planName: 'Six Months Pro', durationMonths: 6, price: 199 },
    { planId: 203, planName: 'Annual Elite', durationMonths: 12, price: 349 }
  ];

  constructor(private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.apiService.getPlans().subscribe(data => {
      if (data && data.length > 0) {
        this.plans = data;
      }
    });
  }

  selectPlan(plan: Plan): void {
    this.router.navigate(['/members'], { queryParams: { planId: plan.planId } });
  }
}
