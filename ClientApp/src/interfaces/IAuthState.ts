import { IUser } from './IUser';

export interface IAuthState {
  user: IUser | null;
  isAuth: boolean;
}
