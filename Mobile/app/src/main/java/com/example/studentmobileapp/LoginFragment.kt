package com.example.studentmobileapp

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.example.studentmobileapp.Models.UserLoginReq
import com.example.studentmobileapp.Utilities.LoginUserHttpService
import com.example.studentmobileapp.Utilities.Settings
import com.example.studentmobileapp.databinding.LoginViewBinding


/**
 * A simple [Fragment] subclass as the default destination in the navigation.
 */
class LoginFragment: Fragment() {

    private var _binding: LoginViewBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = LoginViewBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        (requireActivity() as MainActivity).title = "Login"
        binding.loginBtn.setOnClickListener() {
          if (binding.passwordET.text.isNullOrBlank()) {
            Toast.makeText(requireActivity(), "The password is required!", Toast.LENGTH_SHORT).show()
            return@setOnClickListener
          }

          if (binding.usernameET.text.isNullOrBlank()) {
            Toast.makeText(requireActivity(), "The password is required!", Toast.LENGTH_SHORT).show()
            return@setOnClickListener
          }

          var loginReq: UserLoginReq = UserLoginReq()
          loginReq.username = binding.usernameET.text.toString()
          loginReq.password = binding.passwordET.text.toString()

          LoginUserHttpService(this, loginReq).execute()
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

  public fun onLoginCompleted() {
    Toast.makeText(requireContext(), "Welcome back ${Settings.user!!.username}!", Toast.LENGTH_SHORT).show()
    findNavController().navigate(R.id.action_Login_to_Workshops)
  }
  public fun onLoginError() {
    Toast.makeText(requireContext(), "Failed to login!", Toast.LENGTH_SHORT).show()
  }

//    public fun onGetListCompleted(studentList: MutableList<Student>) {
//        var names = ArrayList<String>()
//        val states = ArrayList<String>()
//        for (i in 0 until studentList.size) {
//            names.add(studentList[i].modifiedName)
//            states.add(studentList[i].state)
//        }
//        val myListAdapter = StudentListAdapter(requireActivity() as MainActivity, names, states)
//        binding.studentListview.adapter = myListAdapter
//        (requireActivity() as MainActivity).binding.fab.isVisible=true
//    }
//    public fun onGetListError() {
//        Snackbar.make(View(requireActivity() as MainActivity), "An error has occurred. Please contact app support for assistance.", Snackbar.LENGTH_LONG)
//            .setAction("Action", null).show()
//    }
}
