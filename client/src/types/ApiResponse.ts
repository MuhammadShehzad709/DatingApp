export type ApiResponse<T> = {
  data: T | null;
  isSuccess: boolean;
  error: string | null;
}
