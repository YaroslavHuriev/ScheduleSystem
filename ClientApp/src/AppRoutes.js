import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { LessonTable } from "./components/LessonTable";
import { Schedule } from "./components/Schedule";
import { SchedulesList } from "./components/SchedulesList";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
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
];

export default AppRoutes;
