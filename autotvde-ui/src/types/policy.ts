export interface PolicyResponse {
  id: string;
  policyNumber: string;
  effectiveFrom: string;
  effectiveTo: string;
  totalPremium: number;
  commission: number;
}
