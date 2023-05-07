import { NbMenuItem } from '@nebular/theme';

export const MENU_ITEMS: NbMenuItem[] = [
  {
    title: 'Home',
    icon: 'home-outline',
    link: '/pages/home',
    home: true,
  },
  {
    title: 'Users',
    icon: 'people-outline',
    link: '/pages/users',
  },
  // {
  //   title: 'Sites',
  //   icon: 'globe-2-outline',
  //   link: '/pages/sites'
  // },
  {
    title: 'Functional Location',
    icon: 'globe-2-outline',
    link: '/pages/functional-location'
  },
  {
    title: 'Mine Information',
    icon: 'globe-2-outline',
    link: '/pages/mine-information'
  },
  {
    title: 'Site Information',
    icon: 'globe-2-outline',
    link: '/pages/site-information'
  },
  {
    title: 'MCP Board',
    icon: 'globe-2-outline',
    link: '/pages/mcp-board'
  },
  {
    title: 'MCP Link',
    icon: 'globe-2-outline',
    link: '/pages/mcp-link'
  },
  {
    title: 'Logout',
    icon: 'log-out-outline',
    link: '/pages/logout'
  },
];
