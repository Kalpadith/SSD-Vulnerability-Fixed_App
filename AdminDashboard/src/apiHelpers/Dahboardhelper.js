import axios from "axios";
import { API_GetCustomerList_URL } from "../constants/constants";

export const fetchUserData = async (token) => {
  try {
    const response = await axios.get(`${API_GetCustomerList_URL}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    return response.data;
  } catch (error) {
    throw error;
  }
};
