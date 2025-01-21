--
-- PostgreSQL database dump
--

-- Dumped from database version 17.2 (Debian 17.2-1.pgdg120+1)
-- Dumped by pg_dump version 17.2 (Debian 17.2-1.pgdg120+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: comment; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.comment (
    id character varying(36) NOT NULL,
    task_id character varying(36),
    description text,
    is_active boolean DEFAULT true,
    is_deleted boolean DEFAULT false,
    creation_date timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    create_by_user character varying(36),
    modification_date timestamp with time zone,
    update_by_user character varying(36)
);


ALTER TABLE public.comment OWNER TO "user";

--
-- Name: history; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.history (
    id character varying(36) NOT NULL,
    task_id character varying(36),
    change_details text,
    is_active boolean DEFAULT true,
    is_deleted boolean DEFAULT false,
    creation_date timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    create_by_user character varying(36),
    modification_date timestamp with time zone,
    update_by_user character varying(36)
);


ALTER TABLE public.history OWNER TO "user";

--
-- Name: profiles; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.profiles (
    id character varying(36) NOT NULL,
    name character varying(255),
    is_active boolean DEFAULT true,
    is_deleted boolean DEFAULT false,
    creation_date timestamp with time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public.profiles OWNER TO "user";

--
-- Name: project; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.project (
    id character varying(36) NOT NULL,
    name character varying(255),
    description text,
    is_active boolean DEFAULT true,
    is_deleted boolean DEFAULT false,
    creation_date timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    create_by_user character varying(36),
    modification_date timestamp with time zone,
    update_by_user character varying(36)
);


ALTER TABLE public.project OWNER TO "user";

--
-- Name: task; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.task (
    id character varying(36) NOT NULL,
    project_id character varying(36) NOT NULL,
    title character varying(255) NOT NULL,
    description text NOT NULL,
    expiration_date timestamp with time zone DEFAULT now() NOT NULL,
    status character varying(50) NOT NULL,
    priority character varying(50) NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    creation_date timestamp with time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    create_by_user character varying(36) NOT NULL,
    modification_date timestamp with time zone,
    update_by_user character varying(36)
);


ALTER TABLE public.task OWNER TO "user";

--
-- Name: user; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public."user" (
    id character varying(36) NOT NULL,
    profile_id character varying(36) NOT NULL,
    first_name character varying(255) NOT NULL,
    last_name character varying(255) NOT NULL,
    email character varying(255) NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    creation_date timestamp with time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    create_by_user character varying(36) NOT NULL,
    modification_date timestamp with time zone,
    update_by_user character varying(36)
);


ALTER TABLE public."user" OWNER TO "user";

--
-- Data for Name: comment; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.comment (id, task_id, description, is_active, is_deleted, creation_date, create_by_user, modification_date, update_by_user) FROM stdin;
\.


--
-- Data for Name: history; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.history (id, task_id, change_details, is_active, is_deleted, creation_date, create_by_user, modification_date, update_by_user) FROM stdin;
6f8b30ae-bf59-4a82-ac6d-5e22539a9e81	123e4567-e89b-12d3-a456-426655440000	Titulo: Tarea 1 -> Tarea 1.100; Descripción: Descripción detallada de la tarea 1. -> Descripción detallada de la tarea 1.100.	t	f	2025-01-20 10:05:34+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
\.


--
-- Data for Name: profiles; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.profiles (id, name, is_active, is_deleted, creation_date) FROM stdin;
550e8400-e29b-41d4-a716-446655440000	usuario	t	f	2025-01-19 18:20:57+00
550e8400-e29b-41d4-a716-446655440001	manager	t	f	2025-01-19 18:20:57+00
550e8400-e29b-41d4-a716-446655440002	admin	t	f	2025-01-19 18:20:57+00
\.


--
-- Data for Name: project; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.project (id, name, description, is_active, is_deleted, creation_date, create_by_user, modification_date, update_by_user) FROM stdin;
0970d093-a488-4d98-affe-8f1fa4e4ddbf	projeto inicial testes	alguma coisa aqui	t	f	2025-01-19 20:43:17+00	123e4567-e89b-12d3-a456-426655440000	2025-01-20 04:11:22+00	123e4567-e89b-12d3-a456-426655440000
a1e12345-f89b-12d3-a456-426655440001	Migração para a Nuvem	Projeto para migração de aplicações para a nuvem Azure	t	f	2025-01-17 17:32:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a2e12345-f89b-12d3-a456-426655440002	Desenvolvimento de Data Lake	Construção de um data lake para análise de dados aprimorada	t	f	2025-01-18 12:45:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a3e12345-f89b-12d3-a456-426655440003	Aprimoramento de Cibersegurança	Melhoria de protocolos de segurança de rede e defesas	t	f	2025-01-19 14:20:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a4e12345-f89b-12d3-a456-426655440004	Treinamento de Modelo de Aprendizado de Máquina	Treinamento de modelos para análises preditivas em vendas	t	f	2025-01-20 11:15:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a5e12345-f89b-12d3-a456-426655440005	Atualização do Sistema ERP	Atualização do sistema ERP para a versão mais recente	t	f	2025-01-21 19:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a6e12345-f89b-12d3-a456-426655440006	Implementação de Sensores IoT	Implementação de sensores IoT para manufatura inteligente	t	f	2025-01-22 16:37:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a7e12345-f89b-12d3-a456-426655440007	Automação de Pipeline DevOps	Automatização de processos de pipeline CI/CD	t	f	2025-01-23 13:08:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a8e12345-f89b-12d3-a456-426655440008	Implementação de Blockchain	Desenvolver soluções de blockchain para a cadeia de suprimentos	t	f	2025-01-24 12:52:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a9e12345-f89b-12d3-a456-426655440009	Desenvolvimento de Chatbot AI	Criação de chatbots AI para melhorar o atendimento ao cliente	t	f	2025-01-25 18:42:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a0e12345-f89b-12d3-a456-426655440010	Expansão de Rede 5G	Expansão de infraestrutura de rede 5G para melhor conectividade	t	f	2025-01-26 20:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
\.


--
-- Data for Name: task; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.task (id, project_id, title, description, expiration_date, status, priority, is_active, is_deleted, creation_date, create_by_user, modification_date, update_by_user) FROM stdin;
d1e12345-f89b-12d3-a456-426655440001	a1e12345-f89b-12d3-a456-426655440001	Configuração de Servidor	Configuração inicial do servidor para deploy.	2025-01-20 13:00:00+00	Pendente	Alta	t	f	2025-01-17 17:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d2e12345-f89b-12d3-a456-426655440002	a1e12345-f89b-12d3-a456-426655440001	Criação de API REST	Desenvolvimento de API REST para integração de serviços.	2025-01-25 21:00:00+00	EmAndamento	Média	t	f	2025-01-18 12:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d3e12345-f89b-12d3-a456-426655440003	a1e12345-f89b-12d3-a456-426655440001	Atualização de Documentação	Revisar e atualizar a documentação técnica.	2025-01-28 15:00:00+00	Concluído	Baixa	t	f	2025-01-19 14:15:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d4e12345-f89b-12d3-a456-426655440004	a1e12345-f89b-12d3-a456-426655440001	Análise de Requisitos	Realizar levantamento detalhado de requisitos para o sistema.	2025-01-22 18:00:00+00	Pendente	Alta	t	f	2025-01-17 18:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d5e12345-f89b-12d3-a456-426655440005	a1e12345-f89b-12d3-a456-426655440001	Teste de Integração	Executar testes de integração entre os módulos do sistema.	2025-01-24 19:00:00+00	EmAndamento	Média	t	f	2025-01-18 13:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d6e12345-f89b-12d3-a456-426655440006	a1e12345-f89b-12d3-a456-426655440001	Configuração de Ambiente	Configurar o ambiente de desenvolvimento para a equipe.	2025-01-26 13:30:00+00	Concluído	Alta	t	f	2025-01-19 11:45:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d7e12345-f89b-12d3-a456-426655440007	a1e12345-f89b-12d3-a456-426655440001	Revisão de Código	Revisar o código enviado pela equipe para manter a qualidade.	2025-01-27 14:00:00+00	Pendente	Média	t	f	2025-01-17 19:20:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d8e12345-f89b-12d3-a456-426655440008	a1e12345-f89b-12d3-a456-426655440001	Planejamento de Sprint	Definir metas e tarefas para a próxima sprint.	2025-01-29 16:00:00+00	EmAndamento	Baixa	t	f	2025-01-18 14:50:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d9e12345-f89b-12d3-a456-426655440009	a1e12345-f89b-12d3-a456-426655440001	Documentação de APIs	Criar e organizar a documentação para as APIs desenvolvidas.	2025-01-30 18:00:00+00	Concluído	Alta	t	f	2025-01-19 17:10:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d1e12345-f89b-12d3-a456-426655440010	a1e12345-f89b-12d3-a456-426655440001	Automação de Testes	Implementar automação para testes de regressão.	2025-01-31 12:30:00+00	Pendente	Alta	t	f	2025-01-17 12:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d1e12345-f89b-12d3-a456-426655440011	a1e12345-f89b-12d3-a456-426655440001	Implementação de CI/CD	Configurar pipelines de CI/CD para deploy contínuo.	2025-02-01 17:00:00+00	EmAndamento	Média	t	f	2025-01-18 15:20:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d1e12345-f89b-12d3-a456-426655440012	a1e12345-f89b-12d3-a456-426655440001	Segurança de Aplicação	Adicionar medidas de segurança para o aplicativo.	2025-02-02 20:30:00+00	Concluído	Alta	t	f	2025-01-19 16:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d1e12345-f89b-12d3-a456-426655440013	a1e12345-f89b-12d3-a456-426655440001	Otimização de Banco de Dados	Realizar tuning no banco para melhorar a performance.	2025-02-03 14:45:00+00	Pendente	Média	t	f	2025-01-17 13:15:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d1e12345-f89b-12d3-a456-426655440014	a1e12345-f89b-12d3-a456-426655440001	Validação de Requisitos	Verificar se os requisitos atendem às expectativas do cliente.	2025-02-04 19:15:00+00	EmAndamento	Baixa	t	f	2025-01-18 17:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d1e12345-f89b-12d3-a456-426655440015	a1e12345-f89b-12d3-a456-426655440001	Treinamento Interno	Capacitar a equipe sobre novas tecnologias utilizadas.	2025-02-05 13:00:00+00	Concluído	Média	t	f	2025-01-19 19:50:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a2e12345-f89b-12d3-a456-426655440010	a2e12345-f89b-12d3-a456-426655440002	Desenvolvimento de Aplicativo Móvel	Desenvolvimento de aplicativo móvel para iOS e Android utilizando Flutter.	2025-03-10 18:00:00+00	EmAndamento	Alta	t	f	2025-01-17 10:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a2e12345-f89b-12d3-a456-426655440011	a2e12345-f89b-12d3-a456-426655440002	Integração de Sistema com API	Integração com a API de terceiros para a troca de informações entre os sistemas.	2025-04-01 16:00:00+00	Pendente	Média	t	f	2025-01-18 11:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a2e12345-f89b-12d3-a456-426655440012	a2e12345-f89b-12d3-a456-426655440002	Criação de Sistema de Análise de Dados	Desenvolvimento de uma plataforma para análise de grandes volumes de dados.	2025-05-10 14:00:00+00	Concluído	Baixa	t	f	2025-01-19 14:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a2e12345-f89b-12d3-a456-426655440013	a2e12345-f89b-12d3-a456-426655440002	Melhorias no Sistema de Banco de Dados	Melhorias na estrutura do banco de dados para reduzir o tempo de resposta.	2025-06-01 09:30:00+00	EmAndamento	Alta	t	f	2025-01-20 12:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a2e12345-f89b-12d3-a456-426655440014	a2e12345-f89b-12d3-a456-426655440002	Automatização de Processos	Automatização de processos repetitivos utilizando robôs de software (RPA).	2025-03-15 11:00:00+00	Pendente	Média	t	f	2025-01-21 13:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a2e12345-f89b-12d3-a456-426655440015	a2e12345-f89b-12d3-a456-426655440002	Criação de API para Autenticação	Desenvolvimento de uma API RESTful para autenticação de usuários.	2025-04-30 12:00:00+00	EmAndamento	Baixa	t	f	2025-01-22 10:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a2e12345-f89b-12d3-a456-426655440016	a2e12345-f89b-12d3-a456-426655440002	Atualização de Sistema de Armazenamento	Planejamento e execução da atualização do sistema de armazenamento de dados.	2025-05-01 14:00:00+00	Pendente	Alta	t	f	2025-01-23 09:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a2e12345-f89b-12d3-a456-426655440017	a2e12345-f89b-12d3-a456-426655440002	Desenvolvimento de Chatbot	Desenvolvimento de um chatbot para atendimento ao cliente utilizando IA.	2025-06-15 11:00:00+00	EmAndamento	Média	t	f	2025-01-24 10:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a2e12345-f89b-12d3-a456-426655440018	a2e12345-f89b-12d3-a456-426655440002	Revisão de Código	Revisão e melhorias no código-fonte do sistema.	2025-07-01 09:00:00+00	Pendente	Alta	t	f	2025-01-25 12:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a3e12345-f89b-12d3-a456-426655440010	a3e12345-f89b-12d3-a456-426655440003	Criação de Sistema de Pagamentos Online	Desenvolvimento de sistema de pagamentos online para e-commerce.	2025-02-15 17:00:00+00	EmAndamento	Alta	t	f	2025-01-18 13:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a3e12345-f89b-12d3-a456-426655440011	a3e12345-f89b-12d3-a456-426655440003	Melhoria de Sistema de Log	Melhorias no sistema de log para facilitar a monitorização e diagnóstico.	2025-03-01 12:00:00+00	Pendente	Média	t	f	2025-01-19 10:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a3e12345-f89b-12d3-a456-426655440012	a3e12345-f89b-12d3-a456-426655440003	Atualização de Sistema de Monitoramento	Atualização do sistema de monitoramento de servidores para maior eficiência.	2025-04-10 15:00:00+00	Concluído	Baixa	t	f	2025-01-20 14:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a3e12345-f89b-12d3-a456-426655440013	a3e12345-f89b-12d3-a456-426655440003	Desenvolvimento de Sistema de Backup	Desenvolvimento de um sistema robusto de backup e recuperação de dados.	2025-05-01 09:00:00+00	EmAndamento	Alta	t	f	2025-01-21 11:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a3e12345-f89b-12d3-a456-426655440014	a3e12345-f89b-12d3-a456-426655440003	Desenvolvimento de Ferramenta de Testes	Desenvolvimento de ferramenta de testes automatizados para integração contínua.	2025-06-10 13:00:00+00	Pendente	Baixa	t	f	2025-01-22 10:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a3e12345-f89b-12d3-a456-426655440015	a3e12345-f89b-12d3-a456-426655440003	Análise de Desempenho de Sistemas	Análise de desempenho de sistemas para detectar gargalos e melhorar a performance.	2025-07-01 16:00:00+00	Pendente	Média	t	f	2025-01-23 12:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a3e12345-f89b-12d3-a456-426655440016	a3e12345-f89b-12d3-a456-426655440003	Desenvolvimento de Sistema de Recomendação	Criação de sistema de recomendação de produtos baseado em IA.	2025-08-01 14:00:00+00	EmAndamento	Alta	t	f	2025-01-24 13:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a3e12345-f89b-12d3-a456-426655440017	a3e12345-f89b-12d3-a456-426655440003	Revisão de Código Fonte	Revisão do código fonte de sistemas legados para melhorar a manutenção.	2025-06-20 12:00:00+00	Pendente	Média	t	f	2025-01-25 14:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a3e12345-f89b-12d3-a456-426655440018	a3e12345-f89b-12d3-a456-426655440003	Desenvolvimento de API de Pagamento	Desenvolvimento de API de pagamento com suporte a múltiplos gateways.	2025-09-01 11:00:00+00	EmAndamento	Alta	t	f	2025-01-26 15:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
b50639d7-7f6e-4e1d-83b6-d5189f2a639f	a4e12345-f89b-12d3-a456-426655440004	Desenvolvimento de API REST	Desenvolver uma API RESTful para integração com sistemas legados.	2025-02-10 15:00:00+00	Pendente	Alta	t	f	2025-01-17 10:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
842c870d-f516-46f9-9e10-4a6c4a911bc4	a4e12345-f89b-12d3-a456-426655440004	Refatoração do sistema de login	Refatorar o sistema de autenticação para melhorar a segurança e desempenho.	2025-03-01 12:00:00+00	EmAndamento	Média	t	f	2025-01-18 12:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
15735b1e-d47d-4f7b-b763-459d65bcd07c	a4e12345-f89b-12d3-a456-426655440004	Correção de bug em sistema de pagamentos	Corrigir erro crítico que impede o processamento de pagamentos de clientes.	2025-02-20 08:00:00+00	Concluído	Alta	t	f	2025-01-19 09:15:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
f76fd14b-1747-417a-bd11-bb46a0aef5b9	a4e12345-f89b-12d3-a456-426655440004	Integração com sistema de pagamentos	Implementar integração com sistema de pagamentos para permitir transações online.	2025-03-15 18:30:00+00	EmAndamento	Média	t	f	2025-01-20 14:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
cb2cc850-5b7d-4403-8721-5c74652f9b08	a4e12345-f89b-12d3-a456-426655440004	Implementação de testes automatizados	Criar uma suíte de testes automatizados para o módulo de pagamentos.	2025-04-01 10:00:00+00	Pendente	Baixa	t	f	2025-01-21 08:45:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
13df6b6b-27d2-4a93-b457-62d280b52251	a4e12345-f89b-12d3-a456-426655440004	Revisão do código de backend	Revisar o código do backend para melhorar a legibilidade e a manutenção do sistema.	2025-02-25 16:00:00+00	Concluído	Média	t	f	2025-01-22 11:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
ff62f208-6b69-45c1-b0c9-1bdfb4e621f7	a4e12345-f89b-12d3-a456-426655440004	Análise de dados de usuários	Realizar análise de dados dos usuários para melhorar a experiência no site.	2025-03-10 14:30:00+00	Pendente	Baixa	t	f	2025-01-23 13:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
bc87f441-55de-4ed1-bf69-dbed7e173b53	a4e12345-f89b-12d3-a456-426655440004	Desenvolvimento de chatbot para atendimento	Desenvolver um chatbot para otimizar o atendimento ao cliente no site.	2025-02-12 17:00:00+00	EmAndamento	Alta	t	f	2025-01-24 15:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
ac742fed-e1f8-4f0d-8193-e728b9b2e3a1	a4e12345-f89b-12d3-a456-426655440004	Refatoração do código de frontend	Refatorar o código frontend para melhorar o desempenho da aplicação web.	2025-03-08 11:45:00+00	Pendente	Média	t	f	2025-01-25 09:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
078ff22f-bdf6-4737-bb2f-f7176d08f394	a4e12345-f89b-12d3-a456-426655440004	Criação de dashboard para relatórios	Criar um dashboard interativo para visualização dos relatórios financeiros.	2025-04-05 13:30:00+00	Concluído	Alta	t	f	2025-01-26 10:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
fada3ab0-bc89-49b8-8b8f-e25697ca5299	a4e12345-f89b-12d3-a456-426655440004	Implementação de notificação por e-mail	Implementar um sistema de notificações por e-mail para os usuários do sistema.	2025-02-28 07:30:00+00	Pendente	Média	t	f	2025-01-27 16:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
11850d1f-bcf2-478d-9c36-b4539237995e	a4e12345-f89b-12d3-a456-426655440004	Melhoria na segurança da API	Melhorar a segurança da API para evitar vulnerabilidades e ataques.	2025-03-12 13:00:00+00	EmAndamento	Alta	t	f	2025-01-28 12:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a05f3f10-4d9d-4c62-baf4-9db619e4b2e5	a4e12345-f89b-12d3-a456-426655440004	Desenvolvimento de sistema de login	Desenvolver um sistema de login para autenticação de usuários na aplicação.	2025-03-18 09:00:00+00	Pendente	Média	t	f	2025-01-29 14:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d8ad2b3f-1be1-496f-a78d-b9d24d628b1d	a4e12345-f89b-12d3-a456-426655440004	Criação de API para integração	Desenvolver uma API para integração com plataformas de pagamento externas.	2025-04-01 11:15:00+00	EmAndamento	Baixa	t	f	2025-01-30 13:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
4d451ed1-e440-4c71-8124-d3eaa071b5fa	a4e12345-f89b-12d3-a456-426655440004	Ajuste de layout do painel	Ajustar o layout do painel de controle para melhorar a usabilidade.	2025-03-25 10:30:00+00	Pendente	Baixa	t	f	2025-02-01 09:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
83f3b20d-0419-4ca3-8f0d-ef55066009bc	a4e12345-f89b-12d3-a456-426655440004	Desenvolvimento de sistema de autenticação	Desenvolver o sistema de autenticação utilizando JWT para segurança do acesso.	2025-04-07 08:45:00+00	Concluído	Alta	t	f	2025-02-02 11:15:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
a803e5a3-1240-4d09-8c9c-77ed2142e5ed	a4e12345-f89b-12d3-a456-426655440004	Desenvolvimento de sistema de recomendação	Desenvolver um sistema de recomendação de produtos baseado em inteligência artificial.	2025-03-30 14:00:00+00	Pendente	Média	t	f	2025-02-03 13:45:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
7d58689e-c218-470d-97d9-2ac6e786e8b4	a4e12345-f89b-12d3-a456-426655440004	Criação de documentação técnica	Criar a documentação técnica para a API de pagamentos para desenvolvedores externos.	2025-04-10 17:00:00+00	EmAndamento	Baixa	t	f	2025-02-04 10:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
110e38c2-6f0d-4c75-81fe-15e9d5a74c9d	a4e12345-f89b-12d3-a456-426655440004	Implementação de busca inteligente	Implementar um sistema de busca inteligente para melhorar a experiência do usuário.	2025-04-12 13:30:00+00	Pendente	Alta	t	f	2025-02-05 12:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
707731d7-96bc-4865-95b2-5e555ffd470d	a7e12345-f89b-12d3-a456-426655440007	Migração de Servidor	Realizar a migração do servidor de produção para um ambiente mais robusto com maior capacidade de processamento	2025-05-01 14:00:00+00	Pendente	Média	t	f	2025-01-21 10:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
c9c77451-a5a4-48dd-bd8d-646478aca935	a7e12345-f89b-12d3-a456-426655440007	Implementação de Log	Desenvolver um sistema de logs para monitoramento e diagnóstico de erros em tempo real	2025-05-10 09:00:00+00	EmAndamento	Alta	t	f	2025-01-19 12:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
15e55190-1f1e-49cb-a33c-f1d85c1cfaa2	a7e12345-f89b-12d3-a456-426655440007	Desenvolvimento de Sistema de Relatórios	Criar um sistema para geração de relatórios gerenciais e analíticos de vendas	2025-04-30 13:30:00+00	Concluído	Baixa	t	f	2025-01-17 15:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
d09996b7-7c0a-4509-8953-72e5ac969955	a5e12345-f89b-12d3-a456-426655440005	Desenvolvimento de API RESTful	Desenvolver uma API RESTful para integração de sistemas internos com autenticação via OAuth2	2025-03-01 10:00:00+00	Pendente	Alta	t	f	2025-01-18 14:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
94aef214-8894-429d-ba2e-e56b78b79c54	a5e12345-f89b-12d3-a456-426655440005	Refatoração do Banco de Dados	Realizar a refatoração do banco de dados para otimização de consultas e normalização das tabelas	2025-03-10 12:00:00+00	EmAndamento	Média	t	f	2025-01-19 09:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
9efea459-848b-4ab3-a028-8a1d5045a78a	a5e12345-f89b-12d3-a456-426655440005	Desenvolvimento de Front-End	Criar a interface de usuário utilizando Angular e Angular Material para o sistema de vendas	2025-02-25 15:00:00+00	Concluído	Alta	t	f	2025-01-17 11:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
bfcc2bd8-dde8-4df4-84f4-41a061f5d7ff	a6e12345-f89b-12d3-a456-426655440006	Automatização de Testes	Implementar um framework de testes automatizados utilizando Selenium para testes de interface	2025-04-01 10:00:00+00	Pendente	Média	t	f	2025-01-20 08:45:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
5bef7f63-826e-491e-b298-ec924a4d217b	a6e12345-f89b-12d3-a456-426655440006	Integração de Sistemas	Desenvolver um sistema para integração de plataformas de pagamento utilizando APIs externas	2025-03-15 11:00:00+00	EmAndamento	Alta	t	f	2025-01-19 13:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
3522cd9b-3406-42cf-9ebe-e5d525887c90	a6e12345-f89b-12d3-a456-426655440006	Ajustes de Performance	Realizar ajustes de performance no back-end para aumentar a escalabilidade da aplicação	2025-04-10 16:00:00+00	Concluído	Baixa	t	f	2025-01-18 10:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
9e5b845b-5226-44f0-bbc5-ab41e726b0a4	a8e12345-f89b-12d3-a456-426655440008	Desenvolvimento de API Restful	Criar uma API RESTful para a integração entre sistemas utilizando JSON e OAuth2	2025-03-01 10:00:00+00	Pendente	Alta	t	f	2025-01-17 14:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
8bd507f6-2993-4229-9aed-c5bad4a58dd6	a8e12345-f89b-12d3-a456-426655440008	Refatoração do Sistema de Bancos de Dados	Melhorar a estrutura do banco de dados para melhorar o desempenho de consultas SQL e índices	2025-03-20 15:00:00+00	EmAndamento	Média	t	f	2025-01-18 10:15:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
4bcabbfd-ae92-4f80-8b5d-cf5b7eefda8e	a8e12345-f89b-12d3-a456-426655440008	Criação de Interface com Angular	Desenvolver interface de usuário responsiva utilizando Angular e Angular Material	2025-03-05 16:00:00+00	Pendente	Alta	t	f	2025-01-19 13:45:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
f6e3f3d3-5703-4438-a264-61ed52e895e0	a8e12345-f89b-12d3-a456-426655440008	Integração de Sistemas de Pagamento	Desenvolver um sistema para integração com APIs de plataformas de pagamento	2025-04-10 17:00:00+00	Concluído	Média	t	f	2025-01-17 12:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
de470cbc-7250-442d-9927-f2cecae2d83e	a8e12345-f89b-12d3-a456-426655440008	Automatização de Testes de Software	Implementar testes automatizados para as funcionalidades críticas da aplicação	2025-04-01 10:00:00+00	EmAndamento	Média	t	f	2025-01-20 14:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
9539cc5e-6c7f-4e7c-bfae-0d6c95fdf757	a8e12345-f89b-12d3-a456-426655440008	Desenvolvimento de Sistema de Logs	Desenvolver um sistema para monitoramento e auditoria de acessos ao sistema	2025-05-01 09:30:00+00	Pendente	Baixa	t	f	2025-01-19 15:20:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
77c245a7-e5d4-4e18-b497-bcacbbfc1b74	a8e12345-f89b-12d3-a456-426655440008	Implementação de Sistema de Notificação	Criar um sistema de notificações em tempo real para os usuários	2025-05-05 11:00:00+00	EmAndamento	Alta	t	f	2025-01-18 16:40:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
4507425c-5152-42cf-9d5d-c486db5b1efa	a8e12345-f89b-12d3-a456-426655440008	Desenvolvimento de Painel de Controle	Criar um painel de controle com visualizações gráficas para métricas do sistema	2025-04-15 14:00:00+00	Concluído	Média	t	f	2025-01-21 10:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
fbb9a439-1718-47b6-815e-55259c157869	a8e12345-f89b-12d3-a456-426655440008	Desenvolvimento de Funcionalidade de Relatórios	Desenvolver funcionalidades para exportação de dados em diversos formatos de relatório	2025-04-20 13:00:00+00	Pendente	Baixa	t	f	2025-01-17 09:10:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
83528fe4-9679-44a8-b634-7ca1e21028b2	a8e12345-f89b-12d3-a456-426655440008	Integração com Sistema de CRM	Desenvolver integração entre o sistema e plataformas de CRM	2025-05-10 12:00:00+00	EmAndamento	Alta	t	f	2025-01-19 08:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
b2ad0303-4950-4ae5-82cc-99ced045e933	a8e12345-f89b-12d3-a456-426655440008	Criação de Ambiente de Testes	Criar ambiente de testes isolado para realizar testes de integração entre microserviços	2025-05-15 18:00:00+00	Concluído	Média	t	f	2025-01-17 16:50:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
9b45e928-8e49-47ef-b890-2a1eff4dfa6c	a8e12345-f89b-12d3-a456-426655440008	Configuração de CI/CD	Implementar pipeline de integração contínua e entrega contínua para o projeto	2025-06-01 09:00:00+00	Pendente	Baixa	t	f	2025-01-20 11:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
f7757798-0fd6-44b1-990c-2b0b34eeb400	a9e12345-f89b-12d3-a456-426655440009	Criação de API de Pagamentos	Desenvolver uma API de pagamentos com integração com gateways de pagamento populares	2025-03-30 16:00:00+00	EmAndamento	Alta	t	f	2025-01-17 13:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
576ceedb-3cb1-4a8a-b7f0-9861a918d72e	a9e12345-f89b-12d3-a456-426655440009	Desenvolvimento de Funcionalidade de Pesquisa	Criar funcionalidades de pesquisa rápida e eficaz no sistema de vendas	2025-04-10 14:30:00+00	Pendente	Média	t	f	2025-01-18 09:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
21c7954c-377e-459e-9f23-4b4634ff658a	a9e12345-f89b-12d3-a456-426655440009	Criação de Sistema de Feedbacks	Desenvolver uma funcionalidade para coleta de feedback dos usuários sobre o sistema	2025-04-25 17:00:00+00	EmAndamento	Alta	t	f	2025-01-19 10:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
764c1314-a490-481a-afb0-d9dd21a2c0ab	a9e12345-f89b-12d3-a456-426655440009	Implementação de Dashboard para Vendas	Desenvolver um painel de controle para exibição de dados analíticos de vendas	2025-05-05 16:00:00+00	Pendente	Baixa	t	f	2025-01-19 11:50:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
2fa15e1b-d35c-4c59-ae60-4e35e1103727	a9e12345-f89b-12d3-a456-426655440009	Migração de Dados	Migrar dados de clientes de sistemas legados para o novo sistema de gerenciamento	2025-05-10 18:00:00+00	Concluído	Média	t	f	2025-01-20 09:40:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
cb5c5df4-9422-47f6-b340-8981e65db424	a9e12345-f89b-12d3-a456-426655440009	Desenvolvimento de Funcionalidade de Histórico	Criar funcionalidades para o histórico de interações dos usuários no sistema	2025-06-01 12:00:00+00	EmAndamento	Alta	t	f	2025-01-21 10:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
89d2f31a-8a93-4926-b91c-09a8ce6f1e44	a9e12345-f89b-12d3-a456-426655440009	Sistema de Autenticação Multifatorial	Implementar autenticação multifatorial para aumentar a segurança do sistema	2025-06-10 10:30:00+00	Pendente	Alta	t	f	2025-01-17 10:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
149149c8-9ccc-4a87-bf5f-d967f4968770	a9e12345-f89b-12d3-a456-426655440009	Implementação de Acesso via API Externa	Desenvolver o sistema para acesso dos usuários via API externa	2025-06-20 14:00:00+00	EmAndamento	Baixa	t	f	2025-01-18 15:10:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
f336a362-9793-47b0-affa-9dc20eeaa622	a9e12345-f89b-12d3-a456-426655440009	Monitoramento de Desempenho	Criar ferramenta de monitoramento de desempenho do sistema para análise de tempo de resposta	2025-06-25 17:30:00+00	Pendente	Média	t	f	2025-01-19 14:10:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
3c4e8804-8c3a-4c29-aa56-9459ebe65870	a9e12345-f89b-12d3-a456-426655440009	Gestão de Erros	Desenvolver um sistema para gerenciar e exibir erros do sistema de forma estruturada	2025-07-01 11:00:00+00	Concluído	Baixa	t	f	2025-01-17 08:30:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
243f2566-12d9-445a-9d1b-81889bd423d2	a9e12345-f89b-12d3-a456-426655440009	Automatização de Processos de Negócio	Automatizar processos de negócio críticos utilizando ferramentas de RPA para redução de erros	2025-07-10 13:00:00+00	Pendente	Alta	t	f	2025-01-20 12:00:00+00	123e4567-e89b-12d3-a456-426655440000	\N	\N
\.


--
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public."user" (id, profile_id, first_name, last_name, email, is_active, is_deleted, creation_date, create_by_user, modification_date, update_by_user) FROM stdin;
123e4567-e89b-12d3-a456-426655440000	550e8400-e29b-41d4-a716-446655440000	Juan	Perez	juan.perez@example.com	t	f	2025-01-19 18:32:12+00	550e8400-e29b-41d4-a716-446655440002	\N	\N
123e4567-e89b-12d3-a456-426655440001	550e8400-e29b-41d4-a716-446655440001	Maria	Lopez	maria.lopez@example.com	t	f	2025-01-19 18:32:12+00	550e8400-e29b-41d4-a716-446655440002	\N	\N
123e4567-e89b-12d3-a456-426655440002	550e8400-e29b-41d4-a716-446655440002	Carlos	Gomez	carlos.gomez@example.com	t	f	2025-01-19 18:32:12+00	550e8400-e29b-41d4-a716-446655440002	\N	\N
\.


--
-- Name: comment comment_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.comment
    ADD CONSTRAINT comment_pkey PRIMARY KEY (id);


--
-- Name: history history_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.history
    ADD CONSTRAINT history_pkey PRIMARY KEY (id);


--
-- Name: profiles profiles_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.profiles
    ADD CONSTRAINT profiles_pkey PRIMARY KEY (id);


--
-- Name: project project_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.project
    ADD CONSTRAINT project_pkey PRIMARY KEY (id);


--
-- Name: task task_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.task
    ADD CONSTRAINT task_pkey PRIMARY KEY (id);


--
-- Name: user user_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pkey PRIMARY KEY (id);


--
-- Name: task task_project_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.task
    ADD CONSTRAINT task_project_id_fkey FOREIGN KEY (project_id) REFERENCES public.project(id);


--
-- Name: user user_profile_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_profile_id_fkey FOREIGN KEY (profile_id) REFERENCES public.profiles(id);


--
-- PostgreSQL database dump complete
--

