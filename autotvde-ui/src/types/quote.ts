// Request para calcular a cotação
export interface QuoteRequest {
  birthDate: string;
  vehiclePowerKw: number;
  vehicleUsage: 'TVDE';
  city: string;
  ncbYears: number;
  hasGlassCoverage: boolean;
  hasRoadsideCoverage: boolean;
}

// Breakdown retornado pelo pricing engine
export interface QuoteBreakdown {
  basePremium: number;
  ageAdjustment: number;
  usageAdjustment: number;
  citySurcharge: number;
  ncbDiscount: number;
  optionalCoverages: number;
  total: number;
}

// Response do endpoint /quotes/price
export interface QuotePriceResponse {
  quoteId: string;
  quoteNumber: string;
  breakdown: QuoteBreakdown;
}

// Response do endpoint /quotes/{id}/bind
export interface BindQuoteResponse {
  id: string;
  policyNumber: string;
  effectiveFrom: string;
  effectiveTo: string;
  totalPremium: number;
  commission: number;
}