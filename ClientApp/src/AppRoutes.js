import { FetchData } from "./components/FetchData";
import { GroupsList } from "./components/GroupsList";
import { Home } from "./components/Home";
import { LessonTable } from "./components/LessonTable";
import { Schedule } from "./components/Schedule";
import { SchedulesList } from "./components/SchedulesList";
import SignIn from "./components/SignIn";
import SignUp from "./components/SignUp";
import { TeachersList } from "./components/TeachersList";

const AppRoutes = [
  {
    element: <Home />
  },
  {
    path: '/fetch-data',
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
  }
];

export default AppRoutes;
