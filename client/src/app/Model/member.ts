import { Photo } from "./photo";

    export interface Member {
        id: number;
        userName: string;
        age: number;
        photoUrl: string;
        passwordHash: string;
        passwordSalt: string;
        dateOfBirth: Date;
        knownAs: string;
        created: Date;
        lastActive: Date;
        gender: string;
        introduction: string;
        city: string;
        lookingFor: string;
        interests: string;
        country: string;
        photos: Photo[];
    }

