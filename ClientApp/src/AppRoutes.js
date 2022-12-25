import { FetchData } from "./components/FetchData";
import { GroupsList } from "./components/GroupsList";
import { LessonTable } from "./components/LessonTable";
import { Schedule } from "./components/Schedule";
import { SchedulesList } from "./components/SchedulesList";
import Settings from "./components/Settings";
import SignIn from "./components/SignIn";
import SignUp from "./components/SignUp";
import { TeachersList } from "./components/TeachersList";

const AppRoutes = [
  {
    path: '/scheduleinputdata',
    element: <FetchData />
  },
  {
    path: '/scheduleinputdata/:id',
    element: <LessonTable />
  },
  {
    path: '/schedule/:id',
    element: <Schedule />
  },
  {
    path: '/schedule',
    element: <SchedulesList />
  },
  {
    path: '/group',
    element: <GroupsList />
  },
  {
    path: '/teacher',
    element: <TeachersList />
  },
  {
    path: '/login',
    element: <SignIn />
  },
  {
    path: '/signup',
    element: <SignUp />
  },
  {
    path: '/settings',
    element: <Settings />
  }
];

export default AppRoutes;
