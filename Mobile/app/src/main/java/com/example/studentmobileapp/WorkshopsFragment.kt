package com.example.studentmobileapp

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import androidx.transition.Visibility
import com.example.studentmobileapp.Utilities.Settings
import com.example.studentmobileapp.databinding.WorkshopsViewBinding


/**
 * A simple [Fragment] subclass as the default destination in the navigation.
 */
class WorkshopsFragment : Fragment() {

  private var _binding: WorkshopsViewBinding? = null

  // This property is only valid between onCreateView and
  // onDestroyView.
  private val binding get() = _binding!!
  override fun onCreateView(
    inflater: LayoutInflater, container: ViewGroup?,
    savedInstanceState: Bundle?
  ): View {
    _binding = WorkshopsViewBinding.inflate(inflater, container, false)
    return binding.root
  }

  override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
    super.onViewCreated(view, savedInstanceState)

    (requireActivity() as MainActivity).title = "Home"

    if (Settings.user == null) {
      binding.workshopReqButton.visibility = View.GONE
      binding.loginReqButton.setOnClickListener() {
        findNavController().navigate(R.id.action_Workshops_to_Login)
      }
    } else {
      binding.loginReqButton.visibility = View.GONE
      binding.workshopReqButton.setOnClickListener() {
        findNavController().navigate(R.id.action_Workshops_to_WorkshopRequest)
      }
    }
  }

  override fun onDestroyView() {
    super.onDestroyView()
    _binding = null
  }

  public fun onLoginCompleted() {
    Toast.makeText(requireContext(), "Welcome back ${Settings.user!!.username}!", Toast.LENGTH_SHORT)
      .show()
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
