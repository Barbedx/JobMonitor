export interface CurentExecutionStatus {
}
export interface RootObject {
  Name: string;
  ServerName: string;
  LastRunDate: Date;
  CurentExecutionStatus: CurentExecutionStatus;
  CurentExecutionStep: string;
  LastRunOutcome: number;
  LastOutcomeMessage: string;
  CurentRetryAttempt: number;
  NextRunDate: Date;
  Enable: boolean;
}
