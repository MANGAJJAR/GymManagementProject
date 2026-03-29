import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Member, CreateMember, Trainer, Plan } from './models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:5220/api'; // Corrected port to match launchSettings.json

  constructor(private http: HttpClient) { }

  // Members
  getMembers(): Observable<Member[]> {
    return this.http.get<Member[]>(`${this.baseUrl}/members`);
  }

  addMember(member: CreateMember): Observable<any> {
    return this.http.post(`${this.baseUrl}/members`, member);
  }

  deleteMember(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/members/${id}`);
  }

  // Trainers
  getTrainers(): Observable<Trainer[]> {
    return this.http.get<Trainer[]>(`${this.baseUrl}/trainers`);
  }

  // Plans
  getPlans(): Observable<Plan[]> {
    return this.http.get<Plan[]>(`${this.baseUrl}/plans`);
  }
}
