package com.example.studentmobileapp.Utilities

import android.os.AsyncTask
import android.util.Log
import com.example.studentmobileapp.LoginFragment
import com.example.studentmobileapp.Models.ApiResponse
import com.example.studentmobileapp.Models.User
import com.example.studentmobileapp.Models.UserLoginReq


class LoginUserHttpService(private var context: LoginFragment, private var req: UserLoginReq): AsyncTask<Void, Void, Boolean>() {
  override fun doInBackground(vararg params: Void?): Boolean {
    val response = HttpPost<ApiResponse<User>, UserLoginReq>(
      "/api/auth/login",
      req,
      listOf(200)
    )

    if (response == null) {
      Log.e("Auth", "Failed to authenticate user")
      return false
    }

    Log.i("Auth", "Authenticated user ${response.data.userId}")
    Settings.user = response.data
    return true
  }

  override fun onPreExecute() {
    super.onPreExecute()
    // ...
  }

  override fun onPostExecute(result: Boolean) {
      super.onPostExecute(result)
      if(result) {
          context.onLoginCompleted();
      }
      else {
          context.onLoginError();
      }
  }
}
