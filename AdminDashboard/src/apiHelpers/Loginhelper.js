import axios from "axios";
import { API_Login_URL } from "../constants/constants";

export const loginhelper = async (username, password) => {
  try {
    const response = await axios.post(`${API_Login_URL}`, {
      username: username,
      password: password,
    });

    return response.data.access_token;
  } catch (error) {
    throw error;
  }
};
