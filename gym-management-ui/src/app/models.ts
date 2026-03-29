export interface Member {
  memberId: number;
  name: string;
  age: number;
  phone: string;
  joinDate: string;
  trainerName?: string;
  planName?: string;
}

export interface CreateMember {
  name: string;
  age: number;
  phone: string;
  trainerId?: number;
  planId: number;
}

export interface Trainer {
  trainerId: number;
  name: string;
  specialization: string;
}

export interface Plan {
  planId: number;
  planName: string;
  durationMonths: number;
  price: number;
}
