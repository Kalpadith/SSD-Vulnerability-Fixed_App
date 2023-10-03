import axios from "axios";
import { API_Login_URL } from "../constants/constants";

export const loginhelper = async (username, password) => {
  try {
    const response = await axios.post("https://localhost:7222/api/User/Login" , {
      username: username,
      password: password,
    });

    return response.data.access_token;
  } catch (error) {
    throw error;
  }
};
