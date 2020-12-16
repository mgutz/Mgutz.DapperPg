create table products (
  id integer generated always as identity (start with 1000) primary key,
  name text not null,
  cost money not null,
  created_at timestamptz default now(),
  updated_at timestamptz default now()
);
